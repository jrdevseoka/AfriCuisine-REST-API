using Africuisine.Application.Config;
using Africuisine.Application.Res;
using Africuisine.Domain.Models;
using Africuisine.Infrastructure.Services.Postmark;
using Microsoft.Extensions.Options;
using PostmarkDotNet;

class PostmarkService : IPostmarkService
{
    private static PostmarkCommand Sender { get; set; }
    private PostmarkClient Postmark { get; set; }

    public PostmarkService(IOptions<PostmarkCommand> options)
    {
        Sender = options.Value;
        Postmark = new(Sender.Key);
    }

    public async Task<BaseResponse> SendTemplateEmail(UserDM recipient, string url, string template)
    {
        var templateMessage = GetTemplatedPostmarkMessage(recipient, url, template);
        var response = await Postmark.SendEmailWithTemplateAsync(templateMessage);
        return new BaseResponse
        {
            Message = response.Message,
            Succeeded = response.Status == PostmarkStatus.Success
        };
    }

    private static TemplatedPostmarkMessage GetTemplatedPostmarkMessage(
        UserDM recipient,
        string url,
        string template
    )
    {
        var templateModel = GetTemplateModel(Sender, recipient.Name, url);
        return new TemplatedPostmarkMessage
        {
            From = Sender.SenderEmail,
            TemplateAlias = template.ToLower(),
            To = recipient.Email,
            TemplateModel = templateModel
        };
    }

    private static Dictionary<string, object> GetTemplateModel(
        PostmarkCommand sender,
        string name,
        string url
    )
    {
        return new Dictionary<string, object>
        {
            { "product_url", sender.CompanyUrl },
            { "product_name", sender.CompanyName },
            { "name", name },
            { "action_url", url },
            { "sender_name", sender.SenderName },
            { "support_email", sender.SupportEmail },
            { "company_name", sender.CompanyName },
            { "company_address", "company_address_Value" },
        };
    }
}
