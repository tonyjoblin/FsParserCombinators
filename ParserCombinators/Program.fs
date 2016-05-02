// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

let testParser str =
    printfn "%A" (parser.A_Parser str)

[<EntryPoint>]
let main argv = 

    testParser ""

    testParser "ABC"

    testParser "ZBC"

    0 // return an integer exit code
