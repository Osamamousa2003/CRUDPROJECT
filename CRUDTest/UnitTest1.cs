 namespace CRUDTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            MyMath myMath = new MyMath();
            int input1 = 10;
            int input2 = 20;
            int expect = 30;
           int result =myMath.Add(input1 , input2);

            Assert.Equal(expect, result);
        }
    }
}