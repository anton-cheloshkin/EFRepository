using EFRepository;

namespace Tests
{
    public class TestExpressions
    {
        [SetUp]
        public void Setup()
        {
            var selector1 = new EntitySelectorModel<One, int>(r => r.Id);
            var selector2 = new EntitySelectorModel<Two, int, int>(r => r.Id, r => r.Value);
        }

        [Test]
        public void OnePK()
        {
            var selector = new EntitySelectorModel<One, int>(r => r.Id);
            var entity = new One { Id = 1 };

            var expr = selector.WithPrimaryKey(1);

            var expr2 = selector.WithPrimaryKey(entity);

            Assert.That(expr.Compile().Invoke(entity), Is.True);
            Assert.That(expr2.Compile().Invoke(entity), Is.True);

            entity.Id = 3;

            Assert.That(expr.Compile().Invoke(entity), Is.False);
            Assert.That(expr2.Compile().Invoke(entity), Is.True);
        }
        [Test]
        public void TwoPK()
        {
            var selector = new EntitySelectorModel<Two, int, int>(r => r.Id, r => r.Value);
            var entity = new Two { Id = 2, Value = 3 };

            var expr = selector.WithPrimaryKey(2, 3);

            var expr2 = selector.WithPrimaryKey(entity);

            Assert.That(expr.Compile().Invoke(entity), Is.True);
            Assert.That(expr2.Compile().Invoke(entity), Is.True);

            entity.Id = 3;

            Assert.That(expr.Compile().Invoke(entity), Is.False);
            Assert.That(expr2.Compile().Invoke(entity), Is.True);
        }
        internal class One
        {
            public int Id { get; set; }
        }
        internal class Two
        {
            public int Id { get; set; }
            public int Value { get; set; }
        }
    }
}