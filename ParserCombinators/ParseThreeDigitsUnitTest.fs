module ParseThreeDigitsUnitTest

open NUnit.Framework
open parser

[<TestFixture>]
type parseThreeDigitsUnitTestFixture() = 

    [<Test>]
    member self.TestParseThreeDigitsSuccess() =
        let input = "123 cat"

        let result = run parseThreeDigitsAsStr input

        match result with
        | Success ("123", " cat") -> () // expected
        | Failure err -> Assert.Fail()
        | _ -> Assert.Fail()

    [<Test>]
    member self.TestParseThreeDigitsFailure() =
        let input = "cat 123"

        let result = run parseThreeDigitsAsStr input

        match result with
        | Failure err -> () // expected Assert.Fail()
        | _ -> Assert.Fail()

    [<Test>]
    member self.TestParseThreeDigitsAsIntSuccess() =
        let input = "123 cat"

        let result = run parseThreeDigitsAsInt input

        match result with
        | Success (123, " cat") -> () // expected
        | Failure err -> Assert.Fail()
        | _ -> Assert.Fail()
