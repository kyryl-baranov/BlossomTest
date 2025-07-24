namespace BlossomTest.Application.Common.Interfaces;
public interface IViewRenderService
{
    Task<string> RenderToStringAsync(string viewName, object model);
}