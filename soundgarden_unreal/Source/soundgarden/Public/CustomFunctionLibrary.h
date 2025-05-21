// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Kismet/BlueprintFunctionLibrary.h"
#include "CustomFunctionLibrary.generated.h"

/**
 * 
 */
UCLASS()
class SOUNDGARDEN_API UCustomFunctionLibrary : public UBlueprintFunctionLibrary
{
	GENERATED_BODY()

public:
	UFUNCTION(BlueprintCallable, Category = "Custom Function Library",
		meta = (Tooltip = "Reorders the given actors array based on their distance to the reference position."))
	static TArray<AActor*> SortActorsByDistance(UPARAM(ref) TArray<AActor*>& Actors, FVector ReferencePosition);
};
