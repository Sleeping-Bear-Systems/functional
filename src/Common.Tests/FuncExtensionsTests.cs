namespace SleepingBear.Functional.Common.Tests;

/// <summary>
///     Tests for <see cref="FuncExtensions" />.
/// </summary>
internal static class FuncExtensionsTests
{
    [Test]
    public static void Identity_ValidatesBehavior()
    {
        var value = new object();
        var result = value.Identity();
        Assert.That(result, Is.SameAs(value));
    }

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

    [Test]
    public static void ToUnary_2Parameters_ValidatesBehavior()
    {
        var unary = FuncExtensions.ToUnaryFunc<int, int, int>(Function, arg2: 2);
        var result = unary(arg: 1);
        Assert.That(result, Is.EqualTo(expected: 1));
        return;

        int Function(int a, int b)
        {
            Assert.That(b, Is.EqualTo(expected: 2));
            return a;
        }
    }

    [Test]
    public static void ToUnary_3Parameters_ValidatesBehavior()
    {
        var unary = FuncExtensions.ToUnaryFunc<int, int, int, int>(Function, arg2: 2, arg3: 3);
        var result = unary(arg: 1);
        Assert.That(result, Is.EqualTo(expected: 1));
        return;

        int Function(int a, int b, int c)
        {
            Assert.Multiple(() =>
            {
                Assert.That(b, Is.EqualTo(expected: 2));
                Assert.That(c, Is.EqualTo(expected: 3));
            });
            return a;
        }
    }

    [Test]
    public static void ToUnary_4Parameters_ValidatesBehavior()
    {
        var unary = FuncExtensions.ToUnaryFunc<int, int, int, int, int>(Function, arg2: 2, arg3: 3, arg4: 4);
        var result = unary(arg: 1);
        Assert.That(result, Is.EqualTo(expected: 1));
        return;

        int Function(int a, int b, int c, int d)
        {
            Assert.Multiple(() =>
            {
                Assert.That(b, Is.EqualTo(expected: 2));
                Assert.That(c, Is.EqualTo(expected: 3));
                Assert.That(d, Is.EqualTo(expected: 4));
            });
            return a;
        }
    }

    [Test]
    public static void ToUnary_5Parameters_ValidatesBehavior()
    {
        var unary = FuncExtensions.ToUnaryFunc<int, int, int, int, int, int>(Function, arg2: 2, arg3: 3, arg4: 4,
            arg5: 5);
        var result = unary(arg: 1);
        Assert.That(result, Is.EqualTo(expected: 1));
        return;

        int Function(int a, int b, int c, int d, int e)
        {
            Assert.Multiple(() =>
            {
                Assert.That(b, Is.EqualTo(expected: 2));
                Assert.That(c, Is.EqualTo(expected: 3));
                Assert.That(d, Is.EqualTo(expected: 4));
                Assert.That(e, Is.EqualTo(expected: 5));
            });
            return a;
        }
    }

    [Test]
    public static void ToUnary_6Parameters_ValidatesBehavior()
    {
        var unary = FuncExtensions.ToUnaryFunc<int, int, int, int, int, int, int>(Function, arg2: 2, arg3: 3, arg4: 4,
            arg5: 5, arg6: 6);
        var result = unary(arg: 1);
        Assert.That(result, Is.EqualTo(expected: 1));
        return;

        int Function(int a, int b, int c, int d, int e, int f)
        {
            Assert.Multiple(() =>
            {
                Assert.That(b, Is.EqualTo(expected: 2));
                Assert.That(c, Is.EqualTo(expected: 3));
                Assert.That(d, Is.EqualTo(expected: 4));
                Assert.That(e, Is.EqualTo(expected: 5));
                Assert.That(f, Is.EqualTo(expected: 6));
            });
            return a;
        }
    }
}