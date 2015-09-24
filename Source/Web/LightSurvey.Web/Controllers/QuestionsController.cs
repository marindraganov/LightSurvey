namespace LightSurvey.Web.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using LightSurvey.Data.Common.Repository;
    using LightSurvey.Web.ViewModels.Questions;
    using LightSurvey.Data.Models;
    using System;

    public class QuestionsController : Controller
    {
        private string questionAddPartial;
        private string questionEditPartial;
        private string questionViewPartial;
        private QuestionType qType;


        private IDeletableEntityRepository<Question> questions;
        private IDeletableEntityRepository<Survey> surveys;

        public QuestionsController(IDeletableEntityRepository<Question> questions, IDeletableEntityRepository<Survey> surveys)
        {
            this.questions = questions;
            this.surveys = surveys; 
        }

        [HttpGet]
        public ActionResult QuestionViewPartial(string surveyNumber, string questionName)
        {
            var question = this.questions.All().Where(q =>  
            q.Name == questionName && q.SurveyNumber == surveyNumber).First();
            SetViewsAccordingQType(question.Type);

            if (question != null)
            {
                var model = MapQuestionToViewModel(question);
                return this.PartialView(questionViewPartial, model);
            }
            else
            {
                return this.Content("You are trying to get nonexistent question!");
            }
        }

        [HttpGet]
        public ActionResult QuestionEditPartial(string surveyNumber, string questionName)
        {
            var question = this.questions.All().Where(q =>
                q.SurveyNumber == surveyNumber &&
                q.Name == questionName).First();

            if (question != null)
            {
                var model = MapQuestionToEditModel(question);

                SetViewsAccordingQType(question.Type);
                return this.PartialView(questionEditPartial, model);
            }

            return this.Content("You are trying to add question to nonexistent survey!");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SRQuestionEditPartial(SRQuestionEditModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                EditQuestion(model);
            }

            return this.QuestionViewPartial(model.SurveyNumber, model.Name);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MCQuestionEditPartial(MCQuestionEditModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                EditQuestion(model);
            }

            return this.QuestionViewPartial(model.SurveyNumber, model.Name);
        }

        [HttpGet]
        public ActionResult QuestionAddPartial(string surveyNumber, string type)
        {
            var survey = this.surveys.All().Where(s => s.SurveyNumber == surveyNumber).First();

            QuestionType qType;
            try 
            { 
                qType = GetQuestionType(type); 
            }
            catch(ArgumentException e)
            {
                return this.Content(e.Message);
            }


            if (survey != null)
            {
                int questionsCount = survey.Questions.Count();
                string questionName = string.Format("Q{0}", questionsCount + 1);

                var model = CreateInputModel(qType);
                model.Name = questionName;
                model.SurveyNumber = surveyNumber;

                SetViewsAccordingQType(qType);
                var result = PartialView(questionAddPartial, model);
                return result;
            }

            return this.Content("You are trying to add question to nonexistent survey!");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SRQuestionAddPartial(SRQuestionInputModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var survey = this.surveys.All().Where(s => s.SurveyNumber == model.SurveyNumber).First();

                //Add new question
                if(survey != null)
                {
                    SRQuestion question = AutoMapper.Mapper.Map<SRQuestion>(model);
                    question.Type = QuestionType.SingleResponse;
                    survey.Questions.Add(question);
                    this.surveys.SaveChanges();
                }
                else
                {
                    return this.Content("You are trying to add question to nonexistent survey!");
                }
            }
            else
            {
                return this.PartialView("_SRQuestionAddPartial", model);
            }

            return this.Content("<h3>The question was saved. Add next question...</h3>");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MCQuestionAddPartial(MCQuestionInputModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var survey = this.surveys.All().Where(s => s.SurveyNumber == model.SurveyNumber).First();

                //Add new question
                if (survey != null)
                {
                    MCQuestion question = AutoMapper.Mapper.Map<MCQuestion>(model);
                    question.Type = QuestionType.MultipleChoise;
                    survey.Questions.Add(question);
                    this.surveys.SaveChanges();
                }
                else
                {
                    return this.Content("You are trying to add question to nonexistent survey!");
                }
            }
            else
            {
                return this.PartialView("_MCQuestionAddPartial", model);
            }

            return this.Content("<h3>The question was saved. Add next question...</h3>");
        }

        private void EditQuestion(QuestionModel model)
        {
            var question = this.questions.All().Where(q =>
                q.SurveyNumber == model.SurveyNumber &&
                q.Name == model.Name).First();

            if (question != null)
            {
                switch (question.Type)
                {
                    case QuestionType.SingleResponse:
                        Mapper.Map<SRQuestionEditModel, SRQuestion>(model as SRQuestionEditModel, question as SRQuestion);
                        break;
                    case QuestionType.MultipleChoise:
                        Mapper.Map<MCQuestionEditModel, MCQuestion>(model as MCQuestionEditModel, question as MCQuestion);
                        break;
                    case QuestionType.DropDownList:
                        break;
                    case QuestionType.DateTime:
                        Mapper.Map<DTQuestionEditModel, DTQuestion>(model as DTQuestionEditModel, question as DTQuestion);
                        break;
                    case QuestionType.SingleTextBox:
                        break;
                    default:
                        break;
                }

                this.questions.SaveChanges();
            }
        }

        private void SetViewsAccordingQType(QuestionType type)
        {
            switch (type)
            {
                case QuestionType.SingleResponse:
                    this.questionAddPartial = "_SRQuestionAddPartial";
                    this.questionEditPartial = "_SRQuestionEditPartial";
                    this.questionViewPartial = "_SRQuestionViewPartial";
                    break;
                case QuestionType.MultipleChoise:
                    this.questionAddPartial = "_MCQuestionAddPartial";
                    this.questionEditPartial = "_MCQuestionEditPartial";
                    this.questionViewPartial = "_MCQuestionViewPartial";
                    break;
                case QuestionType.DropDownList:
                    break;
                case QuestionType.DateTime:
                    this.questionAddPartial = "_DTQuestionAddPartial";
                    this.questionEditPartial = "_DTQuestionEditPartial";
                    this.questionViewPartial = "_DTQuestionViewPartial";
                    break;
                case QuestionType.SingleTextBox:
                    break;
                default:
                    break;
            }
        }

        private QuestionType GetQuestionType(string type)
        {
            switch (type)
            {
                case "SRQ":
                    return QuestionType.SingleResponse;
                case "MCQ":
                    return QuestionType.MultipleChoise;
                case "DDLQ":
                    return QuestionType.DropDownList;
                case "DTQ":
                    return QuestionType.DateTime;
                default:
                    throw new ArgumentException("Unsupported question type!");
            }
        }

        private QuestionModel CreateInputModel(QuestionType qType)
        {
            switch (qType)
            {
                case QuestionType.SingleResponse:
                    return new SRQuestionInputModel();
                case QuestionType.MultipleChoise:
                    return new MCQuestionInputModel();
                case QuestionType.DropDownList:
                    break;
                case QuestionType.DateTime:
                    return new DTQuestionInputModel();
                case QuestionType.SingleTextBox:
                    break;
            }

            throw new InvalidOperationException("Input model can not be created");
        }

        private QuestionModel MapQuestionToViewModel(Question question)
        {
            QuestionModel model;

            switch (question.Type)
            {
                case QuestionType.SingleResponse:
                    model = AutoMapper.Mapper.Map<SRQuestionViewModel>(question);
                    break;
                case QuestionType.MultipleChoise:
                    model = AutoMapper.Mapper.Map<MCQuestionViewModel>(question);
                    break;
                case QuestionType.DropDownList:
                    //model = AutoMapper.Mapper.Map<DDQuestionViewModel>(question);
                    //break;
                case QuestionType.DateTime:
                    model = AutoMapper.Mapper.Map<DTQuestionViewModel>(question);
                    break;
                default:
                    model = null;
                    break;
            }

            return model;
        }

        private QuestionModel MapQuestionToEditModel(Question question)
        {
            QuestionModel model;

            switch (question.Type)
            {
                case QuestionType.SingleResponse:
                    model = AutoMapper.Mapper.Map<SRQuestionEditModel>(question);
                    break;
                case QuestionType.MultipleChoise:
                    model = AutoMapper.Mapper.Map<MCQuestionEditModel>(question);
                    break;
                default:
                    model = null;
                    break;
            }

            return model;
        }
    }
}