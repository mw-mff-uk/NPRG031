dir=$(dirname $0)

mcs "${dir}/alphabet-boosted.cs" -out:"${dir}/alphabet-boosted.exe"
cat "${dir}/input.txt" | mono "${dir}/alphabet-boosted.exe" --print-to-console