
from ursina import *
from perlin_nose import Perlin
from random import random


class MeshTerrain:
    def __init__(this):

        this.block = load_model('block.obj')
        this.textureAtlas = 'texture_atlas_3.png'
        this.numVertices = len(this.block.vertices)
        print(this.numVertices)

        this.subsets = []
        
        this.numSubsets = 10

        this.subWidth = 256

        #our terrain dictionnary "-"
        this.td ={}

        this.perlin = Perlin()

        for i in range(0, this.numSubsets):
            e = Entity(model=Mesh(),
                       texture = this.textureAtlas)
            e.texture_scale*=64/e.texture.width
            this.subsets.append(e)

    def genBlock(this, x, z, y):

        model = this.subsets[0].model  # Lấy mesh từ entity.

        model.vertices.extend([Vec3(x, y, z) + v for v
                              in this.block.vertices])
        # Record terrain in dictionary =)
        this.td["x"+str(floor(x))+
                "y"+str(floor(y))+
                "z"+str(floor(z))] = "t"
        
        #  ngãu nhiên các block
        c = random()-0.5
        model.colors.extend( (Vec4(1-c, 1-c, 1-c, 1),)*
                                this.numVertices )

        # Đây là tọa độ texture atlas cho cỏ.
        uu = 8
        uv = 7
        if y > 2:
            uu = 8
            uv = 6
        model.uvs.extend([Vec2(uu, uv) + u for u in this.block.uvs])



    def geneTerrain(this):
        x = 0
        z = 0
        
        d = int(this.subWidth * 0.5)
        
        for k in range(-d,d):
            for j in range(-d,d):
                # Thay đổi từ y = perlin(x,z) thành z = perlin(x,y)
                z_height = floor(this.perlin.getHeight(x+k, j))
                # Đổi vị trí z và y trong genBlock
                this.genBlock(x+k, j, z_height)

        this.subsets[0].model.generate()