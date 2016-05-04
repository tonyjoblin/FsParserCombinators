module ParserUnitTests

open NUnit.Framework

[<TestFixture>]
type parserFixture() = class
    
    [<Test>]
    member self.TestRun() =
        
        let parserAction input = 
            parser.Success ('a', input)

        let myParser = parser.Parser parserAction
        let input = "something"
        
        let result = parser.run myParser input

        match result with
        | parser.Success ('a', "something") -> () // expected
        | _ -> Assert.Fail()

    [<Test>]
    member self.TestParseAABC() = 
        let input = "ABC"
        let result = parser.run parser.parseA input
        let expectedResult = parser.Success ('A', "BC")
        Assert.AreEqual(expectedResult, result)

    [<Test>]
    member self.TestParseAZBC() = 
        let input = "ZBC"
        let result = parser.run parser.parseA input
        match result with
        | parser.Failure _ -> () // ok
        | parser.Success _ -> Assert.Fail()

    [<Test>]
    member self.TestParseAEmptyString() = 
        let input = ""
        let result = parser.run parser.parseA input
        let expectedResult = parser.Failure "No more input"
        match result with
        | parser.Failure "No more input" -> () // ok
        | _ -> Assert.Fail()

    [<Test>]
    member selft.TestPChar() =
        let parseA = parser.pchar 'A'
        let input = "ABC"
        let result = parser.run parseA input
        let expectedResult = parser.Success ('A', "BC")
        Assert.AreEqual(expectedResult, result)

end
