using Xunit;

// We fake an unreliable third party by abusing static counters, so
// tests should not be run in parallel.

[assembly: CollectionBehavior(DisableTestParallelization = true)]
