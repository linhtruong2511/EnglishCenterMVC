namespace EnglishCenterMVC.Services
{
    public interface IGradingService
    {
        Task RequestGrading(int submissionId);
    }
}
