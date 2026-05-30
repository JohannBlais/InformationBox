using InfoBox;
using NUnit.Framework;

namespace InfoBoxCore.Tests
{
    /// <summary>
    /// Tests for <see cref="InformationBoxScope"/>'s push/pop lifecycle and its
    /// IDisposable implementation - in particular that disposing is idempotent and
    /// does not pop an unrelated scope off the shared stack.
    /// </summary>
    /// <remarks>
    /// <see cref="InformationBoxScope.Current"/> is backed by a process-wide static
    /// stack, so every test captures the baseline scope on entry and asserts the stack
    /// is restored to it, keeping the tests independent of execution order.
    /// </remarks>
    [TestFixture]
    public class InformationBoxScopeTests
    {
        [Test]
        public void Constructor_PushesScopeAsCurrent()
        {
            InformationBoxScope baseline = InformationBoxScope.Current;

            using (InformationBoxScope scope = new InformationBoxScope())
            {
                Assert.That(InformationBoxScope.Current, Is.SameAs(scope));
            }

            Assert.That(InformationBoxScope.Current, Is.SameAs(baseline));
        }

        [Test]
        public void Dispose_PopsScopeRestoringTheParent()
        {
            InformationBoxScope baseline = InformationBoxScope.Current;

            InformationBoxScope outer = new InformationBoxScope();
            InformationBoxScope inner = new InformationBoxScope();
            Assert.That(InformationBoxScope.Current, Is.SameAs(inner));

            inner.Dispose();
            Assert.That(InformationBoxScope.Current, Is.SameAs(outer));

            outer.Dispose();
            Assert.That(InformationBoxScope.Current, Is.SameAs(baseline));
        }

        [Test]
        public void Dispose_CalledTwice_DoesNotPopAnExtraScope()
        {
            InformationBoxScope outer = new InformationBoxScope();
            InformationBoxScope inner = new InformationBoxScope();

            inner.Dispose();
            Assert.That(InformationBoxScope.Current, Is.SameAs(outer));

            // Second dispose must be a no-op; without the disposed guard this would pop
            // the outer scope (or throw on an empty stack).
            inner.Dispose();
            Assert.That(InformationBoxScope.Current, Is.SameAs(outer), "a second Dispose() must not pop the parent scope");

            outer.Dispose();
        }

        [Test]
        public void NestedScopes_DisposeInLifoOrder_RestoreCorrectly()
        {
            InformationBoxScope baseline = InformationBoxScope.Current;

            InformationBoxScope first = new InformationBoxScope();
            InformationBoxScope second = new InformationBoxScope();
            InformationBoxScope third = new InformationBoxScope();

            Assert.That(InformationBoxScope.Current, Is.SameAs(third));

            third.Dispose();
            Assert.That(InformationBoxScope.Current, Is.SameAs(second));

            second.Dispose();
            Assert.That(InformationBoxScope.Current, Is.SameAs(first));

            first.Dispose();
            Assert.That(InformationBoxScope.Current, Is.SameAs(baseline));
        }
    }
}
