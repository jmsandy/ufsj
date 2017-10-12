using System;
using System.ComponentModel;

namespace NetworkBasics.SimpleHttp
{
    /// <summary>
    /// Html Request Options.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>10/12/2017</Date>
    public enum HttpRequestHeaderOptions
    {
        [Description("Http Version")]
        HttpVersion,

        [Description("Method")]
        Method,

        [Description("Address")]
        Address,

        [Description("Accept")]
        Accept,

        [Description("Accept-Charset")]
        AcceptCharset,

        [Description("Accept-Encoding")]
        AcceptEncoding,

        [Description("Accept-Language")]
        AcceptLanguage,

        [Description("Accept-Datetime")]
        AcceptDatetime,

        [Description("AccessControlRequestMethod")]
        AccessControlRequestMethod,

        [Description("Access-Control-Request-Headers")]
        AccessControlRequestHeaders,

        [Description("Authorization")]
        Authorization,

        [Description("Cache-Control")]
        CacheControl,

        [Description("Connection")]
        Connection,

        [Description("Cookie")]
        Cookie,

        [Description("Content-Length")]
        ContentLength,

        [Description("Content-MD5")]
        ContentMD5,

        [Description("Content-Type")]
        ContentType,

        [Description("Date")]
        Date,

        [Description("Expect")]
        Expect,

        [Description("Forwarded")]
        Forwarded,

        [Description("From")]
        From,

        [Description("Host")]
        Host,

        [Description("If-Match")]
        IfMatch,

        [Description("If-Modified-Since")]
        IfModifiedSince,

        [Description("If-None-Match")]
        IfNoneMatch,

        [Description("If-Range")]
        IfRange,

        [Description("If-Unmodified-Since")]
        IfUnmodifiedSince,

        [Description("Max-Forwards")]
        MaxForwards,

        [Description("Origin")]
        Origin,

        [Description("Pragma")]
        Pragma,

        [Description("Proxy-Authorization")]
        ProxyAuthorization,

        [Description("Range")]
        Range,

        [Description("Referer")]
        Referer,

        [Description("TE")]
        TE,

        [Description("User-Agent")]
        UserAgent,

        [Description("Upgrade")]
        Upgrade,

        [Description("Via")]
        Via,

        [Description("Warning")]
        Warning
    }
}
