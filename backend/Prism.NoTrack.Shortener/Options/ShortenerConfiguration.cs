// -----------------------------------------------------------------------
//  <copyright file="ShortenerConfiguration.cs" company="Prism">
//  Copyright (c) Prism. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Prism.NoTrack.Shortener.Options;

public class ShortenerConfiguration
{
    public string? ConnectionString { get; set; }

    public string? ShortDomain { get; set; }
}