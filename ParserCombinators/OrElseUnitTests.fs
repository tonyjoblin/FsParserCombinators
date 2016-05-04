module OrElseUnitTests

open NUnit.Framework
open parser

[<TestFixture>]
type orElseUnitTestFixture() = class

    member self.parseAorB = orElse parseA parseB

    member self.testUsing input =
        run self.parseAorB input
   
    [<Test>]
    member self.TestParseAOrBInAEFSuccess() =
        let input = "AEF"

        let result = self.testUsing input

        match result with
        | Success (('A'), "EF") -> () // expected
        | Failure err -> Assert.Fail()
        | _ -> Assert.Fail()

    member self.TestParseAOrBInBEFSuccess() =
        let input = "BEF"

        let result = self.testUsing input

        match result with
        | Success (('B'), "EF") -> () // expected
        | Failure err -> Assert.Fail()
        | _ -> Assert.Fail()

    member self.TestParseAOrBInXFails() =
        let input = "X"

        let result = self.testUsing input

        match result with
        | Success _ -> Assert.Fail()
        | Failure err -> () // expected

    member self.TestParseAOrBInEmptyStringFails() =
        let input = ""

        let result = self.testUsing input

        match result with
        | Success _ -> Assert.Fail()
        | Failure err -> () // expected

    member self.TestInfixOrElse() =
        let input = "ABC"
        let parser = parseA <|> parseB

        let result = run parser input

        match result with
        | Success (('A'), "BC") -> () // expected
        | Failure err -> Assert.Fail()
        | _ -> Assert.Fail()

end
