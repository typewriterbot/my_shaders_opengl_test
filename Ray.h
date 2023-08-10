#pragma once
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

class Ray {
public:
	glm::vec2 rayOrigin = glm::vec2(0, 0);
	glm::vec2 rayDirection = glm::vec2(2, 2); 
	glm::vec2 normalizeRayDir = normalize(rayDirection); 

	glm::vec3 testVar = glm::vec3(1, 2, 3);
	
	// Ray Algorithm - focus on Path Tracing/Ray Casting
	// position + direction * distance

};
