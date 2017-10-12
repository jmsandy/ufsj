using System;
using System.ComponentModel;

namespace NetworkBasics.SimpleHttp
{
    /// <summary>
    /// Html Response Options.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>10/11/2017</Date>
    public enum HttpResponseHeaderOptions
    {
        [Description("Http Version")]
        HttpVersion,

        [Description("Status Code")]
        StatusCode,

        [Description("Status Message")]
        StatusMessage,

        [Description("Access-Control-Allow-Origin")]
        AccessControlAllowOrigin,

        [Description("Access-Control-Allow-Credentials")]
        AccessControlAllowCredentials,

        [Description("Access-Control-Expose-Headers")]
        AccessControlExposeHeaders,

        [Description("Access-Control-Max-Age")]
        AccessControlMaxAge,

        [Description("Access-Control-Allow-Methods")]
        AccessControlAllowMethods,

        [Description("Access-Control-Allow-Headers")]
        AccessControlAllowHeaders,

        [Description("Accept-Patch")]
        AcceptPatch,

        [Description("Accept-Ranges")]
        AcceptRanges,

        [Description("Age")]
        Age,

        [Description("Allow")]
        Allow,

        [Description("Alt-Svc")]
        AltSvc,

        [Description("Cache-Control")]
        CacheControl,

        [Description("Connection")]
        Connection,

        [Description("Content-Disposition")]
        ContentDisposition,

        [Description("Content-Encoding")]
        ContentEncoding,

        [Description("Content-Language")]
        ContentLanguage,

        [Description("Content-Length")]
        ContentLength,

        [Description("Content-Location")]
        ContentLocation,

        [Description("Content-MD5")]
        ContentMD5,

        [Description("Content-Range")]
        ContentRange,

        [Description("Content-Type")]
        ContentType,

        [Description("Date")]
        Date,

        [Description("ETag")]
        ETag,

        [Description("Expires")]
        Expires,

        [Description("Last-Modified")]
        LastModified,

        [Description("Link")]
        Link,

        [Description("Location")]
        Location,

        [Description("P3P")]
        P3P,

        [Description("Pragma")]
        Pragma,

        [Description("Proxy-Authenticate")]
        ProxyAuthenticate,

        [Description("Public-Key-Pins")]
        PublicKeyPins,

        [Description("Retry-After")]
        RetryAfter,

        [Description("Server")]
        Server,

        [Description("Set-Cookie")]
        SetCookie,

        [Description("Strict-Transport-Security")]
        StrictTransportSecurity,

        [Description("Trailer")]
        Trailer,

        [Description("Transfer-Encoding")]
        TransferEncoding,

        [Description("Tk")]
        Tk,

        [Description("Upgrade")]
        Upgrade,

        [Description("Vary")]
        Vary,

        [Description("Via")]
        Via,

        [Description("Warning")]
        Warning,

        [Description("WWW-Authenticate")]
        WWWAuthenticate,

        [Description("X-Frame-Options")]
        XFrameOptions
    }
}