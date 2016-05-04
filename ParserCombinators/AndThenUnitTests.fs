module AndThenUnitTests

open NUnit.Framework
open parser

[<TestFixture>]
type andThenUnitTestFixture() = class
    
    [<Test>]
    member self.TestParseAThenB() =
        let parseAthenB = andThen parseA parseB
        let input = "ABC"

        let result = run parseAthenB input

        match result with
        | Success (('A', 'B'), "C") -> () // expected
        | Failure err -> Assert.Fail()
        | _ -> Assert.Fail()


    [<Test>]
    member self.TestParseAThenBFailsOnZBC() =
        let parseAthenB = andThen parseA parseB
        let input = "ZBC"

        let result = run parseAthenB input

        match result with
        | Success (('A', 'B'), "C") -> Assert.Fail()
        | Failure err -> () // expected
        | Success _ -> Assert.Fail()


    [<Test>]
    member self.TestInfixAndThen() =
        let parseAthenB = parseA .>>. parseB
        let input = "ABC"

        let result = run parseAthenB input

        match result with
        | Success (('A', 'B'), "C") -> () // expected
        | Failure err -> Assert.Fail()
        | _ -> Assert.Fail()

end
