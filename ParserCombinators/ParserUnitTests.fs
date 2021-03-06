﻿module ParserUnitTests

open NUnit.Framework
open parser

[<TestFixture>]
type parserFixture() = class

    member this.parseA = pchar 'A'
    member self.parseB = pchar 'B'
    
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
        let result = parser.run self.parseA input
        let expectedResult = parser.Success ('A', "BC")
        Assert.AreEqual(expectedResult, result)

    [<Test>]
    member self.TestParseAZBC() = 
        let input = "ZBC"
        let result = parser.run self.parseA input
        match result with
        | parser.Failure _ -> () // ok
        | parser.Success _ -> Assert.Fail()

    [<Test>]
    member self.TestParseAEmptyString() = 
        let input = ""
        let result = parser.run self.parseA input
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

    [<Test>]
    member self.TestChoiceSuccess() =
        let parser = choice [self.parseA; self.parseB]
        let input = "ABC"

        let result = run parser input

        match result with
        | Success ('A', "BC") -> () // expected
        | _ -> Assert.Fail()

    [<Test>]
    member self.TestChoiceFailure() =
        let parser = choice [self.parseA; self.parseB]
        let input = "XYZ"

        let result = run parser input

        match result with
        | Failure err -> () // expected
        | _ -> Assert.Fail()

    [<Test>]
    member self.TestAnyOfSuccess() =
        let parser = anyOf ['A'; 'B']
        let input = "ABC"

        let result = run parser input

        match result with
        | Success ('A', "BC") -> () // expected
        | _ -> Assert.Fail()

    [<Test>]
    member self.TestAnyOfFailure() =
        let parser = anyOf ['A'; 'B']
        let input = "XYZ"

        let result = run parser input

        match result with
        | Failure err -> () // expected
        | _ -> Assert.Fail()

    [<Test>]
    member self.TestParseLowerCaseSuccess() =
        let parser = parseLowerCase
        let input = "aBC"

        let result = run parser input

        match result with
        | Success ('a', "BC") -> () // expected
        | _ -> Assert.Fail()

    [<Test>]
    member self.TestParseLowerCaseFailure() =
        let parser = parseLowerCase
        let input = "ABC"

        let result = run parser input

        match result with
        | Failure err -> () // expected
        | _ -> Assert.Fail()

    member self.TestParseDigitSuccess() =
        let parser = parseDigit
        let input = "1BC"

        let result = run parser input

        match result with
        | Success ('1', "BC") -> () // expected
        | _ -> Assert.Fail()

    [<Test>]
    member self.TestParseDigitFailure() =
        let parser = parseDigit
        let input = "ABC"

        let result = run parser input

        match result with
        | Failure err -> () // expected
        | _ -> Assert.Fail()

end
