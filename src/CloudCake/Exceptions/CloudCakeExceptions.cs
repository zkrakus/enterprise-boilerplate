using System.Runtime.Serialization;

namespace CloudCake.Exceptions;

/// <summary>
/// Base exception type for those are thrown by CloudCake system for CloudCake specific exceptions.
/// </summary>
[Serializable]
public class CloudCakeException : Exception
{
    /// <summary>
    /// Creates a new <see cref="CloudCakeException"/> object.
    /// </summary>
    public CloudCakeException()
    {

    }

    /// <summary>
    /// Creates a new <see cref="CloudCakeException"/> object.
    /// </summary>
    public CloudCakeException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {

    }

    /// <summary>
    /// Creates a new <see cref="CloudCakeException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    public CloudCakeException(string message)
        : base(message)
    {

    }

    /// <summary>
    /// Creates a new <see cref="CloudCakeException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public CloudCakeException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}