namespace SleepingBear.Functional.Common.Tests;

/// <summary>
///     Tests for <see cref="UnitExtensions" />.
/// </summary>
internal static class UnitExtensionsTests
{
    [Test]
    public static void ToFunc_FiveParameters_ValidatesBehavior()
    {
        var actionCalled = false;
        Action<int, int, int, int, int> action = (p1, p2, p3, p4, p5) =>
        {
            actionCalled = true;
            Assert.Multiple(() =>
            {
                Assert.That(p1, Is.EqualTo(expected: 12));
                Assert.That(p2, Is.EqualTo(expected: 34));
                Assert.That(p3, Is.EqualTo(expected: 56));
                Assert.That(p4, Is.EqualTo(expected: 78));
                Assert.That(p5, Is.EqualTo(expected: 90));
            });
        };
        var func = action.ToFunc();
        var returnValue = func(arg1: 12, arg2: 34, arg3: 56, arg4: 78, arg5: 90);
        Assert.Multiple(() =>
        {
            Assert.That(returnValue, Is.EqualTo(Unit.Value));
            Assert.That(actionCalled, Is.True);
        });
    }

    [Test]
    public static void ToFunc_FourParameters_ValidatesBehavior()
    {
        var actionCalled = false;
        Action<int, int, int, int> action = (p1, p2, p3, p4) =>
        {
            actionCalled = true;
            Assert.Multiple(() =>
            {
                Assert.That(p1, Is.EqualTo(expected: 12));
                Assert.That(p2, Is.EqualTo(expected: 34));
                Assert.That(p3, Is.EqualTo(expected: 56));
                Assert.That(p4, Is.EqualTo(expected: 78));
            });
        };
        var func = action.ToFunc();
        var returnValue = func(arg1: 12, arg2: 34, arg3: 56, arg4: 78);
        Assert.Multiple(() =>
        {
            Assert.That(returnValue, Is.EqualTo(Unit.Value));
            Assert.That(actionCalled, Is.True);
        });
    }

    [Test]
    public static void ToFunc_OneParameters_ValidatesBehavior()
    {
        var actionCalled = false;
        Action<int> action = p1 =>
        {
            actionCalled = true;
            Assert.That(p1, Is.EqualTo(expected: 12));
        };
        var func = action.ToFunc();
        var returnValue = func(arg: 12);
        Assert.Multiple(() =>
        {
            Assert.That(returnValue, Is.EqualTo(Unit.Value));
            Assert.That(actionCalled, Is.True);
        });
    }

    [Test]
    public static void ToFunc_SixParameters_ValidatesBehavior()
    {
        var actionCalled = false;
        Action<int, int, int, int, int, int> action = (p1, p2, p3, p4, p5, p6) =>
        {
            actionCalled = true;
            Assert.Multiple(() =>
            {
                Assert.That(p1, Is.EqualTo(expected: 12));
                Assert.That(p2, Is.EqualTo(expected: 34));
                Assert.That(p3, Is.EqualTo(expected: 56));
                Assert.That(p4, Is.EqualTo(expected: 78));
                Assert.That(p5, Is.EqualTo(expected: 90));
                Assert.That(p6, Is.EqualTo(expected: 11));
            });
        };
        var func = action.ToFunc();
        var returnValue = func(arg1: 12, arg2: 34, arg3: 56, arg4: 78, arg5: 90, arg6: 11);
        Assert.Multiple(() =>
        {
            Assert.That(returnValue, Is.EqualTo(Unit.Value));
            Assert.That(actionCalled, Is.True);
        });
    }

    [Test]
    public static void ToFunc_ThreeParameters_ValidatesBehavior()
    {
        var actionCalled = false;
        Action<int, int, int> action = (p1, p2, p3) =>
        {
            actionCalled = true;
            Assert.Multiple(() =>
            {
                Assert.That(p1, Is.EqualTo(expected: 12));
                Assert.That(p2, Is.EqualTo(expected: 34));
                Assert.That(p3, Is.EqualTo(expected: 56));
            });
        };
        var func = action.ToFunc();
        var returnValue = func(arg1: 12, arg2: 34, arg3: 56);
        Assert.Multiple(() =>
        {
            Assert.That(returnValue, Is.EqualTo(Unit.Value));
            Assert.That(actionCalled, Is.True);
        });
    }

    [Test]
    public static void ToFunc_TwoParameters_ValidatesBehavior()
    {
        var actionCalled = false;
        Action<int, int> action = (p1, p2) =>
        {
            actionCalled = true;
            Assert.Multiple(() =>
            {
                Assert.That(p1, Is.EqualTo(expected: 12));
                Assert.That(p2, Is.EqualTo(expected: 34));
            });
        };
        var func = action.ToFunc();
        var returnValue = func(arg1: 12, arg2: 34);
        Assert.Multiple(() =>
        {
            Assert.That(returnValue, Is.EqualTo(Unit.Value));
            Assert.That(actionCalled, Is.True);
        });
    }

    [Test]
    public static void ToFunc_ZeroParameters_ValidatesBehavior()
    {
        var actionCalled = false;
        var action = () => { actionCalled = true; };
        var func = action.ToFunc();
        var returnValue = func();
        Assert.Multiple(() =>
        {
            Assert.That(returnValue, Is.EqualTo(Unit.Value));
            Assert.That(actionCalled, Is.True);
        });
    }
}
