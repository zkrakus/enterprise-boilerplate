using System.Data.Common;
using System.Runtime.Serialization;

namespace CloudCake.Exceptions;

/// <summary>
/// This exception is thrown if a problem on CloudCake initialization progress.
/// </summary>
[Serializable]
public class CloudCakeInitializationException : CloudCakeException
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public CloudCakeInitializationException()
    {

    }

    /// <summary>
    /// Constructor for serializing.
    /// </summary>
    public CloudCakeInitializationException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {

    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message</param>
    public CloudCakeInitializationException(string message)
        : base(message)
    {

    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public CloudCakeInitializationException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}