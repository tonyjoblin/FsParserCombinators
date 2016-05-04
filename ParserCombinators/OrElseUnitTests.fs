module OrElseUnitTests

open NUnit.Framework
open parser

[<TestFixture>]
type andThenUnitTestFixture() = class

    member self.parseAorB = orElse parseA parseB

    member self.testUsing input =
        run self.parseAorB input
   
    [<Test>]
    member self.TestParseAorBinAEFsuccess() =
        let input = "AEF"

        let result = self.testUsing input

        match result with
        | Success (('A'), "EF") -> () // expected
        | Failure err -> Assert.Fail()
        | _ -> Assert.Fail()

    member self.TestParseAorBinBEFsuccess() =
        let input = "BEF"

        let result = self.testUsing input

        match result with
        | Success (('B'), "EF") -> () // expected
        | Failure err -> Assert.Fail()
        | _ -> Assert.Fail()


    [<Test>]
    member self.TestParseAThenBFailsOnZBC() =
        let input = "ZBC"

        let result = self.testUsing input

        match result with
        | Success _ -> Assert.Fail()
        | Failure err -> () // expected


    [<Test>]
    member self.TestInfixAndThen() =
        let parseAorB = parseA <|> parseB
        let input = "ABC"

        let result = run parseAorB input

        match result with
        | Success (('A'), "BC") -> () // expected
        | Failure err -> Assert.Fail()
        | _ -> Assert.Fail()

end
