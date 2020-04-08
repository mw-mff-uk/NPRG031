data <- read.csv("obchod/out/out.csv", header=TRUE)

data$t <- as.numeric(data$t)
data$n <- as.numeric(data$n)
data$k <- as.numeric(data$k)

colors <- c("#003f5c", "#2f4b7c", "#665191", "#a05195", "#d45087", "#f95d6a", "#ff7c43", "#ffa600")

par(cex=1.3)

i <- 1
ks <- rev(as.numeric(rownames(table(data$k[data$k > 5 | data$k == 0]))))
for (k in ks) {
  subset <- data[data$k == k, ]
  if (i == 1) {
    plot(subset$n, subset$t, col=colors[i], lwd=3, type="l", main="PDKZSVOD (K > 5)", xlab="Počet zákazníkov", ylab="PDKZSVOD")
  } else {
    lines(subset$n, subset$t, col=colors[i], lwd=3, type="l")
  }
  i <- i + 1
}
legend("topleft", as.character(ks), fill=colors)


for (k in c(1:5, 0)) {
  subset <- data[data$k == k, ]
  if (k == 1) {
    plot(subset$n, subset$t, col=colors[k+1], lwd=3, type="l", main="PDKZSVOD (K <= 5)", xlab="Počet zákazníkov", ylab="PDKZSVOD")
  } else {
    lines(subset$n, subset$t, col=colors[k+1], lwd=3, type="l")
  }
}
legend("topleft", as.character(c(1:5, 0)), fill=colors[c(2:6, 1)])