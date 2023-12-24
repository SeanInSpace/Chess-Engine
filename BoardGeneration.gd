extends Node2D

@export_category("Init Variants")
@export var boardSprite2D: Sprite2D
@export var cell_container: Node2D
@export var BoardSizeXY := 32
@export var ViewNoise := false
@export var RegenerateCells := false
@export var SquareSize := 16
var CellPositionOffset := Vector2(SquareSize, SquareSize)

@export var colorBlack := Color.BLACK
@export var colorWhite := Color.WHITE
@export var colorGray := Color.DIM_GRAY

#region Noise Variants
@export_category("Noise Variants")
@export var cellular_distance_function: int
@export var cellular_jitter: float
@export var cellular_return_type: int
@export var domain_warp_amplitude: float
@export var domain_warp_enabled: bool
@export var domain_warp_fractal_gain: float
@export var domain_warp_fractal_lacunarity: float
@export var domain_warp_fractal_octaves: int
@export var domain_warp_fractal_type: int
@export var domain_warp_frequency: float
@export var domain_warp_type: int
@export var fractal_gain: float
@export var fractal_lacunarity: float
@export var fractal_octaves: int
@export var fractal_ping_pong_strength: float
@export var fractal_type: int
@export var fractal_weighted_strength: float
@export var frequency: float
@export var noise_type: int
@export var offset: Vector3
@export var NoiseSeed := 0
#endregion < Noise Variants >

var BaseNoise: FastNoiseLite
var BoardImage: Image
var BoardTexture: ImageTexture

func _ready():
	BaseNoise = FastNoiseLite.new()
	initNoise()

func _process(delta):
	if Input.is_action_just_pressed("NEW_BOARD"):
		initNoise()

func initNoise():
	BaseNoise.set_seed(NoiseSeed)
	BoardImage = BaseNoise.get_image(BoardSizeXY, BoardSizeXY)
	
	if ViewNoise == true:
		BoardTexture = ImageTexture.create_from_image(BoardImage)
		boardSprite2D.set_texture(BoardTexture)

	if RegenerateCells == true:
		GenerateCells(BaseNoise)

func GenerateCells(FromNoise):
	for i in BoardImage.get_height():
		for j in BoardImage.get_width():
			var cellPos = Vector2(i, j)
			var cell = ColorRect.new()
			cell.set_name("Cell: " + str(cellPos))
			cell.set_size(Vector2(16, 16))
			cell.set_position(cellPos * CellPositionOffset)
			cell.set_color(GetCellColor(cellPos))
			cell_container.add_child(cell)

func GetCellColor(cellPos) -> Color:
	var PosVal: int = (cellPos.x + cellPos.y)
	if PosVal % 2 == 0:
		return colorWhite
	else:
		return colorBlack



