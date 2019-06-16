# sample-xunit-unreliable-fact

Example of an UnreliableFact xUnit attribute implementation.

## Description

This project is an example of how to create a custom xUnit FactAttribute.
The attribute allows you to consider specific types of exceptions and then:

- Retry tests failing with those exceptions for a max number of times
- Decide to even ad-hoc skip those tests for the given exceptions

Making it effectively a combination of the `RetryFact` and `SkippableFact` from [the xUnit samples project](https://github.com/xunit/samples.xunit).
