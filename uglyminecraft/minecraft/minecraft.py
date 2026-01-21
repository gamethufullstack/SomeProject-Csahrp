from ursina import *
from ursina.prefabs.first_person_controller import FirstPersonController
from mesh_terrain import MeshTerrain

app = Ursina()

window.color = color.rgb(0, 200, 255)
indra = Sky()
indra.color = window.color
subject = FirstPersonController()
subject.gravity = 0.0
subject.cursor.visible = False
window.fullscreen = True

terrain = MeshTerrain()

# Tốc độ cơ bản và tốc độ chạy nhanh
normal_speed = 5  # Tốc độ bình thường
sprint_speed = 10  # Tốc độ chạy nhanh

def update():
    step = 2
    height = 1.86
    x = str(floor(subject.x + 0.5))
    z = str(floor(subject.z + 0.5))
    y = floor(subject.y + 0.5)

    block_found = False
    target = subject.y

    # Điều chỉnh tốc độ di chuyển khi nhấn Shift + W
    if held_keys['shift'] and held_keys['w']:
        subject.speed = sprint_speed
    else:
        subject.speed = normal_speed

    for i in range(-step, step):
        if terrain.td.get(f"x{x}y{y + i}z{z}") == "t":
            target = y + i + height
            block_found = True
            break

    if block_found:
        subject.y = lerp(subject.y, target, 6 * time.dt)
    else:
        subject.y -= 9.8 * time.dt

terrain.geneTerrain()

app.run()
