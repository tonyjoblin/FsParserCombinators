module ParserUnitTests

open NUnit.Framework

[<TestFixture>]
type parserFixture() = class
    
    [<Test>]
    member self.TestAparserABC() =
        let input = "ABC"

        let result = parser.A_Parser input

        Assert.AreEqual((true, "BC"), result)

    [<Test>]
    member self.TestAparserZBC() =
        let input = "ZBC"

        let result = parser.A_Parser input

        Assert.AreEqual((false, input), result)

    [<Test>]
    member self.TestAparserEmptyString() =
        let input = System.String.Empty

        let result = parser.A_Parser input

        Assert.AreEqual((false, input), result)

    [<Test>]
    member self.TestAparserNullString() =
        let input = null

        let result = parser.A_Parser input

        Assert.AreEqual((false, System.String.Empty), result)

    [<Test>]
    member self.TestPcharABC() = 
        let input = "ABC"
        let (msg, remaining) = parser.pchar 'A' input
        Assert.AreEqual("BC", remaining)

    [<Test>]
    member self.TestPcharZBC() = 
        let input = "ZBC"
        let (msg, remaining) = parser.pchar 'A' input
        Assert.AreEqual("ZBC", remaining)

    [<Test>]
    member self.TestPcharEmptyString() = 
        let input = ""
        let (msg, remaining) = parser.pchar 'A' input
        Assert.AreEqual("", remaining)

end
