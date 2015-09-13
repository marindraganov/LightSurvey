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

        private QuestionModel MapQuestionToViewModel(Question question)
        {
            QuestionModel model;

            switch (question.Type)
            {
                case QuestionType.SingleResponse:
                    model = AutoMapper.Mapper.Map<SRQuestionViewModel>(question);
                    break;
                default:
                    model = null;
                    break;
            }

            return model;
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
                SetViewsAccordingQType(qType);
                int questionsCount = survey.Questions.Count();
                string questionName = string.Format("Q{0}", questionsCount + 1);

                var model = CreateInputModel();
                model.Name = questionName;
                model.SurveyNumber = surveyNumber;

                SetViewsAccordingQType(qType);
                var result = PartialView(questionAddPartial, model);
                return result;
            }

            return this.Content("You are trying to add question to nonexistent survey!");
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
                    break;
                case QuestionType.DropDownList:
                    break;
                case QuestionType.DateTime:
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

        private QuestionModel CreateInputModel()
        {
            switch (this.qType)
            {
                case QuestionType.SingleResponse:
                    return new SRQuestionInputModel();
                case QuestionType.MultipleChoise:
                    break;
                case QuestionType.DropDownList:
                    break;
                case QuestionType.DateTime:
                    break;
                case QuestionType.SingleTextBox:
                    break;
            }

            throw new InvalidOperationException("Input model can not be created");
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

        [HttpGet]
        public ActionResult QuestionEditPartial(string surveyNumber, string questionName)
        {
            var question = this.questions.All().Where(q => 
                q.SurveyNumber == surveyNumber && 
                q.Name == questionName).First();

            if (question != null)
            {
                var model = AutoMapper.Mapper.Map<SRQuestionEditModel>(question);

                return this.PartialView("_SRQuestionEditPartial", model);
            }

            return this.Content("You are trying to add question to nonexistent survey!");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SRQuestionEditPartial(SRQuestionEditModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var question = this.questions.All().Where(q =>
                q.SurveyNumber == model.SurveyNumber &&
                q.Name == model.Name).First() as SRQuestion;

                if (question != null)
                {
                    SRQuestion editedQuestion = AutoMapper.Mapper.Map<SRQuestion>(model);
                    Mapper.Map<SRQuestionEditModel, SRQuestion>(model,question);

                    this.questions.SaveChanges();
                }
                else
                {
                    return this.Content("You are trying to edit nonexistant!");
                }
            }

            return this.QuestionViewPartial(model.SurveyNumber, model.Name);
        }
    }
}