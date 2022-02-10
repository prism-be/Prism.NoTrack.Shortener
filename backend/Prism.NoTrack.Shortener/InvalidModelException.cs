// -----------------------------------------------------------------------
//  <copyright file="InvalidModelException.cs" company="Prism">
//  Copyright (c) Prism. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Prism.NoTrack;

using System.Runtime.Serialization;

[Serializable]
public class InvalidModelException : Exception
{
    public InvalidModelException(Dictionary<string, string[]> validations) : this("The model is invalid", validations)
    {
    }

    public InvalidModelException(string message, Dictionary<string, string[]> validations) : base(message)
    {
        this.Validations = validations;
    }

    protected InvalidModelException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        this.Validations = info.GetValue(nameof(this.Validations), typeof(Dictionary<string, string[]>)) as Dictionary<string, string[]> ?? new Dictionary<string, string[]>();
    }

    public Dictionary<string, string[]> Validations { get; }
}