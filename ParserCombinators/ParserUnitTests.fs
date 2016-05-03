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
        let result = parser.pchar 'A' input
        let expectedResult = parser.Success ('A', "BC")
        Assert.AreEqual(expectedResult, result)

    [<Test>]
    member self.TestPcharZBC() = 
        let input = "ZBC"
        let result = parser.pchar 'A' input
        match result with
        | parser.Failure _ -> () // ok
        | parser.Success _ -> Assert.Fail()

    [<Test>]
    member self.TestPcharEmptyString() = 
        let input = ""
        let result = parser.pchar 'A' input
        let expectedResult = parser.Failure "No more input"
        match result with
        | parser.Failure "No more input" -> () // ok
        | _ -> Assert.Fail()

end
