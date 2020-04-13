dir=$(dirname $0)

mcs "${dir}/alphabet.cs" -out:"${dir}/alphabet.exe"
cat "${dir}/input.txt" | mono "${dir}/alphabet.exe" --print-to-console