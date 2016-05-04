module AndThenUnitTests

open NUnit.Framework
open parser

[<TestFixture>]
type andThenUnitTestFixture() = class

    member self.parseA = pchar 'A'
    member self.parseB = pchar 'B'
    member self.parseAthenB = andThen self.parseA self.parseB
    
    [<Test>]
    member self.TestParseAThenB() =
        let input = "ABC"

        let result = run self.parseAthenB input

        match result with
        | Success (('A', 'B'), "C") -> () // expected
        | Failure err -> Assert.Fail()
        | _ -> Assert.Fail()


    [<Test>]
    member self.TestParseAThenBFailsOnZBC() =
        let input = "ZBC"

        let result = run self.parseAthenB input

        match result with
        | Success (('A', 'B'), "C") -> Assert.Fail()
        | Failure err -> () // expected
        | Success _ -> Assert.Fail()


    [<Test>]
    member self.TestInfixAndThen() =
        let parseAthenB = self.parseA .>>. self.parseB
        let input = "ABC"

        let result = run parseAthenB input

        match result with
        | Success (('A', 'B'), "C") -> () // expected
        | Failure err -> Assert.Fail()
        | _ -> Assert.Fail()

end
