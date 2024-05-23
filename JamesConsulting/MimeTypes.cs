// <copyright file="MimeTypes.cs" company="James Consulting LLC">
// Copyright © James Consulting LLC. All rights reserved.
// </copyright>

namespace JamesConsulting;

/// <summary>
/// This class contains nested classes that represent different categories of MIME types.
/// Each nested class contains constants that represent specific MIME types within that category.
/// </summary>
public static class MimeTypes
{
    /// <summary>
    /// This class contains constants that represent application-specific MIME types.
    /// </summary>
    public static class Application
    {
        /// <summary>
        /// Represents the MIME type for Atom Syndication Format in applications.
        /// </summary>
        public const string AtomcatXml = "application/atomcat+xml";

        /// <summary>
        /// Represents the MIME type for Atom Syndication Format in applications.
        /// </summary>
        public const string AtomXml = "application/atom+xml";

        /// <summary>
        /// Represents the MIME type for ECMAScript (JavaScript) in applications.
        /// </summary>
        public const string Ecmascript = "application/ecmascript";

        /// <summary>
        /// Represents the MIME type for Java Archive (JAR) in applications.
        /// </summary>
        public const string JavaArchive = "application/java-archive";

        /// <summary>
        /// Represents the MIME type for JavaScript in applications.
        /// </summary>
        public const string Javascript = "application/javascript";

        /// <summary>
        /// Represents the MIME type for JSON format in applications.
        /// </summary>
        public const string Json = "application/json";

        /// <summary>
        /// Represents the MIME type for JSON Patch format in applications.
        /// </summary>
        public const string JsonPatch = "application/json-patch+json";

        /// <summary>
        /// Represents the MIME type for MP4 format in applications.
        /// </summary>
        public const string Mp4 = "application/mp4";

        /// <summary>
        /// Represents the MIME type for octet-stream format in applications.
        /// </summary>
        public const string OctetStream = "application/octet-stream";

        /// <summary>
        /// Represents the MIME type for PDF format in applications.
        /// </summary>
        public const string Pdf = "application/pdf";

        /// <summary>
        /// Represents the MIME type for PKCS #10 format in applications.
        /// </summary>
        public const string Pkcs10 = "application/pkcs10";

        /// <summary>
        /// Represents the MIME type for PKCS #7 mime format in applications.
        /// </summary>
        public const string Pkcs7Mime = "application/pkcs7-mime";

        /// <summary>
        /// Represents the MIME type for PKCS #7 signature format in applications.
        /// </summary>
        public const string Pkcs7Signature = "application/pkcs7-signature";

        /// <summary>
        /// Represents the MIME type for PKCS #8 format in applications.
        /// </summary>
        public const string Pkcs8 = "application/pkcs8";

        /// <summary>
        /// Represents the MIME type for PostScript format in applications.
        /// </summary>
        public const string Postscript = "application/postscript";

        /// <summary>
        /// Represents the MIME type for RDF/XML format in applications.
        /// </summary>
        public const string RdfXml = "application/rdf+xml";

        /// <summary>
        /// Represents the MIME type for RSS/XML format in applications.
        /// </summary>
        public const string RssXml = "application/rss+xml";

        /// <summary>
        /// Represents the MIME type for RTF format in applications.
        /// </summary>
        public const string Rtf = "application/rtf";

        /// <summary>
        /// Represents the MIME type for SMIL/XML format in applications.
        /// </summary>
        public const string SmilXml = "application/smil+xml";

        /// <summary>
        /// Represents the MIME type for OTF font format in applications.
        /// </summary>
        public const string XFontOtf = "application/x-font-otf";

        /// <summary>
        /// Represents the MIME type for TTF font format in applications.
        /// </summary>
        public const string XFontTtf = "application/x-font-ttf";

        /// <summary>
        /// Represents the MIME type for WOFF font format in applications.
        /// </summary>
        public const string XFontWoff = "application/x-font-woff";

        /// <summary>
        /// Represents the MIME type for XHTML/XML format in applications.
        /// </summary>
        public const string XhtmlXml = "application/xhtml+xml";

        /// <summary>
        /// Represents the MIME type for XML format in applications.
        /// </summary>
        public const string Xml = "application/xml";

        /// <summary>
        /// Represents the MIME type for XML DTD format in applications.
        /// </summary>
        public const string XmlDtd = "application/xml-dtd";

        /// <summary>
        /// Represents the MIME type for PKCS #12 format in applications.
        /// </summary>
        public const string XPkcs12 = "application/x-pkcs12";

        /// <summary>
        /// Represents the MIME type for Adobe Flash format in applications.
        /// </summary>
        public const string XShockwaveFlash = "application/x-shockwave-flash";

        /// <summary>
        /// Represents the MIME type for Microsoft Silverlight format in applications.
        /// </summary>
        public const string XSilverlightApp = "application/x-silverlight-app";

        /// <summary>
        /// Represents the MIME type for XSLT/XML format in applications.
        /// </summary>
        public const string XsltXml = "application/xslt+xml";

        /// <summary>
        /// Represents the MIME type for ZIP format in applications.
        /// </summary>
        public const string Zip = "application/zip";
    }

    /// <summary>
    /// This class contains constants that represent audio-specific MIME types.
    /// </summary>
    public static class Audio
    {
        /// <summary>
        /// Represents the MIME type for MIDI format in audio.
        /// </summary>
        public const string Midi = "audio/midi";

        /// <summary>
        /// Represents the MIME type for MP4 format in audio.
        /// </summary>
        public const string Mp4 = "audio/mp4";

        /// <summary>
        /// Represents the MIME type for MPEG format in audio.
        /// </summary>
        public const string Mpeg = "audio/mpeg";

        /// <summary>
        /// Represents the MIME type for OGG format in audio.
        /// </summary>
        public const string Ogg = "audio/ogg";

        /// <summary>
        /// Represents the MIME type for WebM format in audio.
        /// </summary>
        public const string Webm = "audio/webm";

        /// <summary>
        /// Represents the MIME type for AAC format in audio.
        /// </summary>
        public const string XAac = "audio/x-aac";

        /// <summary>
        /// Represents the MIME type for AIFF format in audio.
        /// </summary>
        public const string XAiff = "audio/x-aiff";

        /// <summary>
        /// Represents the MIME type for M3U (MPEG URL) format in audio.
        /// </summary>
        public const string XMpegurl = "audio/x-mpegurl";

        /// <summary>
        /// Represents the MIME type for WMA (Windows Media Audio) format in audio.
        /// </summary>
        public const string XMsWma = "audio/x-ms-wma";

        /// <summary>
        /// Represents the MIME type for WAV format in audio.
        /// </summary>
        public const string XWav = "audio/x-wav";
    }

    /// <summary>
    /// This class contains constants that represent image-specific MIME types.
    /// </summary>
    public static class Image
    {
        /// <summary>
        /// Represents the MIME type for BMP format in images.
        /// </summary>
        public const string Bmp = "image/bmp";

        /// <summary>
        /// Represents the MIME type for GIF format in images.
        /// </summary>
        public const string Gif = "image/gif";

        /// <summary>
        /// Represents the MIME type for JPEG format in images.
        /// </summary>
        public const string Jpeg = "image/jpeg";

        /// <summary>
        /// Represents the MIME type for PNG format in images.
        /// </summary>
        public const string Png = "image/png";

        /// <summary>
        /// Represents the MIME type for SVG/XML format in images.
        /// </summary>
        public const string SvgXml = "image/svg+xml";

        /// <summary>
        /// Represents the MIME type for TIFF format in images.
        /// </summary>
        public const string Tiff = "image/tiff";

        /// <summary>
        /// Represents the MIME type for WebP format in images.
        /// </summary>
        public const string Webp = "image/webp";
    }

    /// <summary>
    /// This class contains constants that represent multipart-specific MIME types.
    /// </summary>
    public static class Multipart
    {
        /// <summary>
        /// Represents the MIME type for form data in multipart.
        /// </summary>
        public const string FormData = "multipart/form-data";
    }

    /// <summary>
    /// This class contains constants that represent text-specific MIME types.
    /// </summary>
    public static class Text
    {
        /// <summary>
        /// Represents the MIME type for CSS format in text.
        /// </summary>
        public const string Css = "text/css";

        /// <summary>
        /// Represents the MIME type for CSV format in text.
        /// </summary>
        public const string Csv = "text/csv";

        /// <summary>
        /// Represents the MIME type for HTML format in text.
        /// </summary>
        public const string Html = "text/html";

        /// <summary>
        /// Represents the MIME type for plain text.
        /// </summary>
        public const string Plain = "text/plain";

        /// <summary>
        /// Represents the MIME type for Rich Text Format in text.
        /// </summary>
        public const string RichText = "text/richtext";

        /// <summary>
        /// Represents the MIME type for SGML format in text.
        /// </summary>
        public const string Sgml = "text/sgml";

        /// <summary>
        /// Represents the MIME type for YAML format in text.
        /// </summary>
        public const string Yaml = "text/yaml";
    }

    /// <summary>
    /// This class contains constants that represent video-specific MIME types.
    /// </summary>
    public static class Video
    {
        /// <summary>
        /// Represents the MIME type for H.264 format in video.
        /// </summary>
        public const string H264 = "video/h264";

        /// <summary>
        /// Represents the MIME type for MP4 format in video.
        /// </summary>
        public const string Mp4 = "video/mp4";

        /// <summary>
        /// Represents the MIME type for MPEG format in video.
        /// </summary>
        public const string Mpeg = "video/mpeg";

        /// <summary>
        /// Represents the MIME type for OGG format in video.
        /// </summary>
        public const string Ogg = "video/ogg";

        /// <summary>
        /// Represents the MIME type for QuickTime format in video.
        /// </summary>
        public const string Quicktime = "video/quicktime";

        /// <summary>
        /// Represents the MIME type for 3GPP format in video.
        /// </summary>
        public const string Threegpp = "video/3gpp";

        /// <summary>
        /// Represents the MIME type for WebM format in video.
        /// </summary>
        public const string Webm = "video/webm";
    }
}