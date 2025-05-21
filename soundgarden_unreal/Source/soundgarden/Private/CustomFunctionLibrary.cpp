// Fill out your copyright notice in the Description page of Project Settings.


#include "CustomFunctionLibrary.h"

TArray<AActor*> UCustomFunctionLibrary::SortActorsByDistance(TArray<AActor*>& Actors, FVector ReferencePosition)
{
	//Sort using Lambda function to compare distances
	Actors.Sort([ReferencePosition](AActor& A, AActor& B)
	{
		float DistA = FVector::DistSquared(A.GetActorLocation(), ReferencePosition);
		float DistB = FVector::DistSquared(B.GetActorLocation(), ReferencePosition);
		return DistA < DistB; //Ascending order (closest first)
	});

	return Actors;
}
