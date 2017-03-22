// used to store any Value - Text set of data
namespace Questionnaire.DataBroker.Model
{
    public interface IValueTextPair
    {
        string text { get; set; }

        string value { get; set; }       
    }
}
