open InputUtils
open Extensions
open Matrix

let part1 lines =

    let rec traverse (currDir: string list) (dirs: (string list * int) list) (lines: string list) =
        match lines with
        | [] -> dirs
        | head::tail ->
            match head with
            | "$ cd /" -> traverse [] dirs tail
            | "$ cd .." -> traverse (List.tail currDir) dirs tail
            | StrPrefix "$ cd " dir -> traverse (dir::currDir) dirs tail
            | "$ ls"
            | StrPrefix "dir" _ -> traverse currDir dirs tail
            | Regex @"(\d+) .+" [_; size] -> traverse currDir ((currDir, int size.Value)::dirs) tail
            | x -> failwithf "Could not parse line: '%s'" x

    let addSubdirSizes (map: Map<string, int>) (dir, size) =
        dir
        |> List.fold (fun (m: Map<string,int>) dirName ->
                m.Change (dirName, Option.orElse (Some 0) >> Option.map ((+)size))
            ) map

    traverse [] [] lines
    |> List.fold addSubdirSizes Map.empty
    |> Map.values
    |> Seq.filter ((>=) 100_000)
    |> Seq.sum

[<EntryPoint>]
let main argv =
    let input =
        System.IO.File.ReadLines (getFileName argv)
        |> List.ofSeq

    part1 input
    |> printfn "%A"
    //|> Seq.iter (printfn "%A")

    0

