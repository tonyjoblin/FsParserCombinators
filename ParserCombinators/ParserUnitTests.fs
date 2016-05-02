module ParserUnitTests

open NUnit.Framework

[<TestFixture>]
type parserFixture() = class
    
    [<Test>]
    member self.TestABC() =
        let input = "ABC"

        let result = parser.A_Parser input

        Assert.AreEqual((true, "BC"), result)

    [<Test>]
    member self.TestZBC() =
        let input = "ZBC"

        let result = parser.A_Parser input

        Assert.AreEqual((false, input), result)

    [<Test>]
    member self.TestEmptyString() =
        let input = System.String.Empty

        let result = parser.A_Parser input

        Assert.AreEqual((false, input), result)

    [<Test>]
    member self.TestNullString() =
        let input = null

        let result = parser.A_Parser input

        Assert.AreEqual((false, System.String.Empty), result)

end
