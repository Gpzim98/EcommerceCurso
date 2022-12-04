#!/usr/bin/env bash
# exit when any command fails
set -e

APPNAME="ecommerceappcurso-python"

cd ../src
func azure functionapp publish $APPNAME
