open InputUtils

let part1 str =
    let idx =
        str
        |> List.ofSeq
        |> List.windowed 4
        |> List.findIndex (fun window -> (window |> Set.ofList |> Set.count) = 4)
    idx + 4

let part2 str =
    let idx =
        str
        |> List.ofSeq
        |> List.windowed 14
        |> List.findIndex (fun window -> (window |> Set.ofList |> Set.count) = 14)
    idx + 14

[<EntryPoint>]
let main argv =
    let input =
        System.IO.File.ReadLines (getFileName argv)
        |> Seq.exactlyOne
    
    printfn "%d" (part1 input)
    printfn "%d" (part2 input)

    0

