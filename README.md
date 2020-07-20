# Video Analysis

tflite_convert --graph_def_file=%OUTPUT_DIR%/tflite_graph.pb --output_file=%OUTPUT_DIR%/detect.tflite --enable_v1_converter --input_shapes=1,300,300,3 --input_arrays=normalized_input_image_tensor --output_arrays=TFLite_Detection_PostProcess,TFLite_Detection_PostProcess:1,TFLite_Detection_PostProcess:2,TFLite_Detection_PostProcess:3 --inference_type=QUANTIZED_UINT8 --mean_values=128 --std_dev_values=128 --change_concat_input_ranges=false --allow_custom_ops
