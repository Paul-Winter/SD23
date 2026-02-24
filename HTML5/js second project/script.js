var bulletX = 30;
var bulletY = 50;
var onFly = false;
//var rand = Math.round(Math.random()*4-2);

function randomMove(){
    return Math.round(Math.random()*4-2);
}

function moveBullet(Y) {
    if(onFly) {
        bulletX += 5;
        bulletY += Y;
        if(bulletX >= 245) {
            bulletX = 30;
            bulletY = 50;
            onFly = false;
        }
        //setTimeout(moveBullet, 50, Y);        
        else {
            setTimeout(moveBullet, 50, Y);
        }
    }
    bullet.style.left = bulletX + "px";
    bullet.style.top = bulletY + "px";
}

function keyHandler(e) {
    if(e.code == "Space" && !onFly) {
        onFly = true;
        moveBullet(randomMove());
    }
    if(e.code == "KeyW" && !onFly) {
    onFly = true;
    moveBullet(-1);
    }
    if(e.code == "KeyS" && !onFly) {
    onFly = true;
    moveBullet(0);
    }
    if(e.code == "KeyX" && !onFly) {
    onFly = true;
    moveBullet(1);
    }
}
