#!/usr/bin/env bash
# exit when any command fails
set -e

APPNAME="ecommerceappcurso"

cd ../
func azure functionapp publish $APPNAME
