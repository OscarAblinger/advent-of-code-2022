open InputUtils
open Extensions
open Extensions.Seq
open System.Data.SqlTypes
open Matrix

let part1 (lines: string seq seq): int =
    lines
    |> Seq.map (Seq.map int)
    |> Seq.map (Seq.sum)
    |> Seq.max

let part2 (lines: string seq seq): int =
    lines
    |> Seq.map (Seq.map int)
    |> Seq.map (Seq.sum)
    |> Seq.sortDescending
    |> Seq.take 3
    |> Seq.sum

[<EntryPoint>]
let main argv =
    let input =
        System.IO.File.ReadLines (getFileName argv)
        |> splitWhen ((=) "")
        |> Seq.cache

    part1 input
    |> printfn "%d"
    
    part2 input
    |> printfn "%d"

    0

