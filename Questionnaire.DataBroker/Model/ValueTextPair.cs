// used to store any Value - Text set of data
namespace Questionnaire.DataBroker.Model
{
    public interface IValueTextPair
    {
        string value { get; set; }

        string text { get; set; }
    }
}
