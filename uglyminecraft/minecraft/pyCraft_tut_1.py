
from ursina import *  # Nhập thư viện Ursina để tạo ứng dụng 3D đơn giản
from ursina.prefabs.first_person_controller import FirstPersonController  # Nhập bộ điều khiển góc nhìn thứ nhất
from numpy import floor  # Nhập hàm floor để làm tròn xuống số thực
from numpy import abs
import time
from perlin_noise import PerlinNoise  # Nhập PerlinNoise để tạo địa hình ngẫu nhiên

# Khởi tạo ứng dụng Ursina
app = Ursina()

# Thiết lập màu nền cửa sổ
window.color = color.rgb(0, 200, 111)

# Ẩn nút thoát (exit button) của cửa sổ
window.exit_button.visible = False

prevTime = time.time()

# Thiết lập sương mù (fog) cho cảnh với màu và mật độ nhất định
scene.fog_color = color.rgb(255, 255, 255)
scene.fog_density = 0.01

# Tải texture cho cỏ
grassStrokeTex = load_texture('grass_14.png')

# Hàm xử lý đầu vào của người chơi
def input(key):
    if key == 'q' or key == 'escape':  # Nhấn 'q' hoặc 'escape' để thoát
        quit()
    if key == 'g': genarateSubset()


# Hàm update (dùng để cập nhật logic mỗi khung hình, nhưng hiện tại chưa có gì)
def update():
    global prevZ, prevX, prevTime
    if  abs(subject.z - prevZ) > 1 or \
        abs(subject.x - prevX) > 1:
        generateshell()

    if time.time() - prevTime > 0.5:
        genarateSubset()

# Khởi tạo noise Perlin với độ phức tạp và seed cố định
noise = PerlinNoise(octaves=2, seed=2021)

# Thiết lập biên độ và tần số của địa hình
amp = 32     # Biên độ (độ cao tối đa của địa hình)
freq = 100  # Tần số (mức độ chi tiết của noise)

# Tạo thực thể terrain để chứa địa hình
terrain = Entity(model=None, collider=None)
# # Chiều rộng của địa hình
terrainWidth = 100
subWidth  = terrainWidth
subsets = []
subCubes = []
sci = 0 # subCube index
currentSubset = 0

for i in range(subWidth):
    bud = Entity(model='cube')
    subCubes.append(bud)

for i in range(int((terrainWidth * terrainWidth) / subWidth)):
    bud = Entity(model=None)
    bud.parent = terrain
    subsets.append(bud)

def genarateSubset():
    global sci, currentSubset, freq, amp
    if currentSubset >= len(subsets):
        finishTerrain()
        return
    for i in range(subWidth):
        x = subCubes[i].x = floor((i+sci)/terrainWidth)
        z = subCubes[i].z = floor((i+sci)%terrainWidth)
        y = subCubes[i].y = floor((noise([x / freq, z / freq])) * amp)
        subCubes[i].parent = subsets[currentSubset]
        subCubes[i].color = color.green
        subCubes[i].visible  = False

    subsets[currentSubset].combine(auto_destroy=False)
    subsets[currentSubset].texture = grassStrokeTex
    sci += subWidth
    currentSubset += 1
terrainFinished =False
def finishTerrain():
    global terrainFinished
    if terrainFinished==True: return
    application.pause()
    terrain.combine()
    terrainFinished = True
    subject.y = 32
    terrain.texture = grassStrokeTex
    application.resume()

#
# # Vòng lặp tạo các khối (cubes) để xây dựng địa hình
for i in range(terrainWidth * terrainWidth):
     bud = Entity(model='cube', color=color.green)  # Tạo khối cube màu xanh lá
     bud.x = floor(i / terrainWidth)  # Xác định tọa độ x theo chỉ số i
     bud.z = floor(i % terrainWidth)  # Xác định tọa độ z theo chỉ số i
     bud.y = floor((noise([bud.x / freq, bud.z / freq])) * amp)  # Tạo độ cao y dựa trên noise
     bud.parent = terrain  # Đặt khối này làm con của terrain
#
# # Kết hợp tất cả các khối thành một thực thể duy nhất để tối ưu hóa
terrain.combine()
#
# # Đặt collider cho terrain (để phát hiện va chạm)
terrain.collider = 'mesh'
#
# # Gán texture cho terrain
terrain.texture = grassStrokeTex

shellies = []
shellWidth = 6
for i in range(shellWidth*shellWidth):
    bud = Entity(model='cube', collider='box')
    bud.visible=False
    shellies.append(bud)

def generateshell():
    global shellWidth, amp, freq
    for i in range(len(shellies)):
        x= shellies[i].x = floor((i/shellWidth) +
                                 subject.x  - 0.5*shellWidth)
        z= shellies[i].z = floor((i % shellWidth) +
                                 subject.z - 0.5*shellWidth)
        shellies[i].y = floor((noise([x / freq, z/ freq])) * amp)

# Tạo nhân vật điều khiển góc nhìn thứ nhất
subject = FirstPersonController()

# Ẩn con trỏ chuột khi điều khiển nhân vật
subject.cursor.visible = False

# Thiết lập trọng lực cho nhân vật
subject.gravity = 1.0

# Đặt vị trí ban đầu cho nhân vật
subject.x = subject.z = 5
subject.y = 12

prevZ = subject.z
prevX = subject.x

# chickenModel = load_model('\minecraft\minecraft\minecraft-chicken\source\chicken.obj-')
# vincent = Entity(model=chickenModel, scale=1,
#                   x=22,z=16,y=7.1,
#                   texture='chick.png',
#                   double_sided=True)

generateshell()
# Chạy ứng dụng
app.run()