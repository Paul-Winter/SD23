function colorDIV(e) {
    DIV.style.left = e.pageX + "px";
    DIV.style.top = e.pageY + "px";
    DIV.style.backgroundColor="rgb(" +
        Math.round(255*Math.random()) + "," +
        Math.round(255*Math.random()) + "," +
        Math.round(255*Math.random()) + ")";
}
