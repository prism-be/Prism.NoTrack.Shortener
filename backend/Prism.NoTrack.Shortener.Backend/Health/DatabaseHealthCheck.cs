// -----------------------------------------------------------------------
//  <copyright file="DatabaseHealthCheck.cs" company="Prism">
//  Copyright (c) Prism. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Prism.NoTrack.Shortener.Backend.Health;

using MediatR;

using Microsoft.Extensions.Diagnostics.HealthChecks;

using Prism.NoTrack.Shortener.Queries;

public class DatabaseHealthCheck: IHealthCheck
{
    private readonly IMediator _mediator;

    public DatabaseHealthCheck(IMediator mediator)
    {
        this._mediator = mediator;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var documentCounts = await this._mediator.Send(new GetDocumentsCount(), cancellationToken);

        return HealthCheckResult.Healthy($"Number of document in database: {documentCounts}");
    }
}