using System.Globalization;

namespace SleepingBear.Functional.Common.Tests;

/// <summary>
///     Tests for <see cref="PipeExtensions" />.
/// </summary>
internal static class PipeExtensionsTests
{
    [Test]
    public static void Pipe_ValidatesBehavior()
    {
        var funcCalled = false;
        var piped = 1234.Pipe(v =>
        {
            funcCalled = true;
            Assert.That(v, Is.EqualTo(expected: 1234));
            return v.ToString(CultureInfo.InvariantCulture);
        });
        Assert.Multiple(() =>
        {
            Assert.That(piped, Is.EqualTo(expected: "1234"));
            Assert.That(funcCalled, Is.True);
        });
    }

    [Test]
    public static void PipeIf_ConditionFalse_ValidatesBehavior()
    {
        var funcCalled = false;
        var piped = 1234.PipeIf(
            condition: false,
            v =>
            {
                funcCalled = true;
                Assert.That(v, Is.EqualTo(expected: 1234));
                return v + 1;
            });
        Assert.Multiple(() =>
        {
            Assert.That(piped, Is.EqualTo(expected: 1234));
            Assert.That(funcCalled, Is.False);
        });
    }

    [Test]
    public static void PipeIf_ConditionTrue_ValidatesBehavior()
    {
        var funcCalled = false;
        var piped = 1234.PipeIf(
            condition: true,
            v =>
            {
                funcCalled = true;
                Assert.That(v, Is.EqualTo(expected: 1234));
                return v + 1;
            });
        Assert.Multiple(() =>
        {
            Assert.That(piped, Is.EqualTo(expected: 1235));
            Assert.That(funcCalled, Is.True);
        });
    }

    [Test]
    public static void PipeIf_ErrorFuncFalse_ValidatesBehavior()
    {
        var funcCalled = false;
        var piped = 1234.PipeIf(
            v =>
            {
                Assert.That(v, Is.EqualTo(expected: 1234));
                return false;
            },
            v =>
            {
                funcCalled = true;
                return v + 1;
            });
        Assert.Multiple(() =>
        {
            Assert.That(piped, Is.EqualTo(expected: 1234));
            Assert.That(funcCalled, Is.False);
        });
    }

    [Test]
    public static void PipeIf_ErrorFuncTrue_ValidatesBehavior()
    {
        var funcCalled = false;
        var piped = 1234.PipeIf(
            v =>
            {
                Assert.That(v, Is.EqualTo(expected: 1234));
                return true;
            },
            v =>
            {
                funcCalled = true;
                Assert.That(v, Is.EqualTo(expected: 1234));
                return v + 1;
            });
        Assert.Multiple(() =>
        {
            Assert.That(piped, Is.EqualTo(expected: 1235));
            Assert.That(funcCalled, Is.True);
        });
    }
}