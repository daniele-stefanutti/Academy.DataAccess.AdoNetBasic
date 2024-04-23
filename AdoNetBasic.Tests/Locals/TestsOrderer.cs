using Xunit.Abstractions;
using Xunit.Sdk;

namespace AdoNetBasic.Tests.Locals;

public class TestsOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase
    {
        foreach (TTestCase testCase in testCases.OrderBy(testCase => testCase.TestMethod.Method.Name))
        {
            yield return testCase;
        }
    }
}
