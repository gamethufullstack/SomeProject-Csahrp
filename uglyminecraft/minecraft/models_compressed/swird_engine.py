

class swirlEngine:
    def __init__(this, subWidth, Vec2):

        this.subWidth = subWidth

        this.run = 1
        this.iteration = 0
        this.count = 0

        this.pos = Vec2(0, 0)

        this.cd = 0
        this.dir = [Vec2(0,1),
                    Vec2(1,0),
                    Vec2(0, -1),
                    Vec2(-1, 0)]

    def ChangeDir(this):
        if this.cd < 3:
            this.cd+=1
        else:
            this.cd = 0
            this.iteration+=1

        if this.cd < 2:
            this.run = (this.iteration * 2) - 1
        else:
            this.run= (this.iteration * 2)

    def move(this):
        if this.count < this.run:
            this.pos.x + this.dir[this.cd]*this.subWidth
            this.pos.x + this.dir[this.cd]*this.subWidth
            this.count +=1
        else:
            this.ChangeDir()