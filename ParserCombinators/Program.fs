﻿// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open parser

[<EntryPoint>]
let main argv = 

    let parseAThenB = andThen parseA parseB

    0 // return an integer exit code
