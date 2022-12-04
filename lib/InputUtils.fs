module InputUtils

open System

let getFileName (argv: string array) =
    if argv.Length <> 0 then
        argv.[0]
    else
        "data/source.txt"

let waitForUser () =
    printfn "Finished. Press 'e' to exit the program…"

    let rec doUntil (f: unit -> ConsoleKeyInfo) expected =
        if f().KeyChar <> expected then
            doUntil f expected

    doUntil System.Console.ReadKey 'e'

