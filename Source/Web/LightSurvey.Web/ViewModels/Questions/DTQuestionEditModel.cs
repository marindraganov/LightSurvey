using System.ComponentModel.DataAnnotations;
namespace LightSurvey.Web.ViewModels.Questions
{
    public class DTQuestionEditModel : QuestionModel
    {
        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}