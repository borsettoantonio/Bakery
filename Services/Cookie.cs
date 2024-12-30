using System.Security.Cryptography.Xml;

namespace Select.Services;
public class Cookie
{
    private readonly IHttpContextAccessor httpContextAccessor;
    public Cookie(IHttpContextAccessor _httpContextAccessor)
    {
        httpContextAccessor = _httpContextAccessor;
    }

    public void Set(string key, string value, int? expireTime)
    {
        CookieOptions option = new CookieOptions();

        if (expireTime.HasValue)
            option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
        // else
        //     option.Expires = DateTime.Now.AddMilliseconds(10);

        httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
    }
    public void Remove(string key)
    {
        httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
    }
    public string Get(string key)
    {
        return httpContextAccessor.HttpContext.Request.Cookies[key];
    }
}