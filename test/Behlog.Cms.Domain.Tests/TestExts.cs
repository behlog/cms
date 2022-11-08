using System.Linq;
using Xunit;
using Behlog.Core.Contracts;
using Behlog.Core.Domain;
using FluentAssertions;

namespace Behlog.Cms.Domain.Tests;

public static class TestExtensions
{

    public static void ShouldContainsExpectedDomainEvent<TId, TExpectation>(
        this IAggregateRoot<TId> aggregateRoot,
        TExpectation expectation,
        string because = "",
        params object[] becauseArgs) where TExpectation : BehlogDomainEvent
    {
        aggregateRoot.GetAllEvents()
            .First(_ => _.GetType() == expectation.GetType())
            .Should().NotBeNull();
        aggregateRoot.GetAllEvents()
            .Where(_ => _.GetType() == expectation.GetType())
            .Should().ContainEquivalentOf(
                expectation, opt =>
                    opt.Excluding(_ => _.EventId)
                        .Excluding(_ => _.EventVersion)
                        .Excluding(_ => _.EventPublishedAt),
                because, becauseArgs);
    }
}